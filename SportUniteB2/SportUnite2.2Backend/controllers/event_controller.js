const Event = require('../models/event');
const http = require('http');
const neo = require('../databases/neo.js').driver;
const mail = require('../email/mailgun');


module.exports = {

	get(req, res, next) {

		const apiResponse = [];
		let promiseArray = [];
		let eventsArray = [];

		let promise = new Promise((resolve, reject) => {
			http.get('http://sportuniteapi.azurewebsites.net/api/event', (response) => {
				let events = '';

				response.on('data', (data) => {
					events += data;
				});

				response.on('end', () => {
					resolve(events);
				})
			})
				.on("error", (error) => {
					reject(error);
				})
		})
			.then((events) => {
				let parsedEvents = JSON.parse(events);
				parsedEvents._embedded.events.forEach((event) => {
					let promise = Event.findOne({eventId: event.eventId});
					promiseArray.push(promise);
					eventsArray.push(event);
				});
				return Promise.all(promiseArray);
			})
			.then((responses) => {
				responses.forEach((response) => {
					if (response !== null) {
						eventsArray.forEach((event, index) => {
							if (event.eventId === response.eventId) {
								eventsArray.splice(index, 1);
								if (response !== null) {
									apiResponse.push({event: event, usernames: response.usernames, sportcomplex: response.sportcomplex, date: response.date, long: response.long, lat: response.lat});

								} else {
									apiResponse.push({event: event, usernames: []});
								}
							}
						});
					} else {
						apiResponse.push({event: eventsArray[0], usernames: []});
// =======
// 						apiResponse.push({event: eventsArray[0], users: []});
// >>>>>>> master
						eventsArray.splice(0, 1);
					}
				});
				res.send(apiResponse);
			})
			.catch((next));
	},


	getOne(req, res, next) {

		let request = 'http://sportuniteapi.azurewebsites.net/api/Event/' + req.params.id;
		let event = '';
		new Promise((resolve, reject) => {
			http.get(request, (response) => {
                response.on('data', (data) => {
                    event += data;
                });

                response.on('end', () => {
                    resolve(event);
                })
			}).on("error", (error) => {
                reject(error);
            })
		}).then((response) => {
            let parsedEvent = JSON.parse(response);
            Event.findOne({eventId: parsedEvent.eventId})
				.then((result) => {
            		res.send({event: parsedEvent, usernames: result.usernames, sportcomplex: result.sportcomplex, date: result.date, long: result.long, lat: result.lat});
				})
				.catch(() => {
            		res.status(404).send();
				})
        }).catch(() => {
			res.status(404).send();
		})

	},

	getEventUsers(req, res, next) {

        let response;


		Event
			.findOne({ eventId: req.params.id })
			.then((result) => {
				response = result.usernames;
			})
			.then(() => res.status(200).json(response))
			.catch((error) => res.status(400).json(error));
	},

    post(req, res, next) {
		const event = req.body;

		Event.create(event)
			.then((createdEvent) => res.status(201).send(createdEvent))
			.catch((next));
	},

	put(req, res, next) {
		const id = req.params.id;
		const event = req.body;

		Event.findOneAndUpdate({eventId: id}, event)
			.then((response) => Event.findById(response._id))
			.then((updatedEvent) => res.send(updatedEvent))
			.catch((next));
	},

	delete(req, res, next) {
		const id = req.params.id;

		Event.findOneAndRemove({eventId: id})
			.then((deletedEvent) => res.status(204).send())
			.catch((next));
	},

	join(req, res, next) {
		const id = req.params.id;
		const body = req.body;

		const event = { eventId: id, usernames: body.usernames };


		console.log(event);


		Event.findOne({eventId: id})
			.then((response) => {
				if (response === null) {
					return Event.create(event);
				} else {
					return Event.findOneAndUpdate({ eventId: id }, event, {new: true});
				}
			})
			.then((response) => {
				res.send(response);
			})
			.catch((next));

	},

	sendMail(req, res, next) {
		let body = mail(req.body.subject, req.body.to, req.body.message);
		res.send(body);

	}
};