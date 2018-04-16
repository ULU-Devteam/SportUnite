const mongoose = require('mongoose');
const assert = require('assert');
const request = require('supertest');
const app = require('../app');
const token = require('./test_helper');

const Event = mongoose.model('event');

describe('Events controller receiving', () => {

	beforeEach((done) => {

		let testEvent = new Event({eventId: 8, usernames: [{username: 'testuser1'}, {username: 'testuser2'}]});

		testEvent.save()
			.then(() => done());
	});

	// xit('a GET request to /api/event/:id returns an event or an error if event with given ID doesn\'t exist', (done) => {
    //
	// 	//TODO: needs to be updated for new endpoint
    //
	// });

	it('a POST request to /api/events creates an event', (done) => {

		request(app)
			.post('/api/event')
			.expect(201)
			.set('token', token)
			.send({eventId: 4, usernames: [{username: 'testinguser3'}, {username: 'testinguser4'}]})
			.then((response) => {
				assert(response.body.eventId === 4);
				assert(response.body.usernames.length === 2);
				assert(response.body.usernames[0].username === 'testinguser3');
				assert(response.body.usernames[1].username === 'testinguser4');
				done();
			});

	});

	it('a PUT request to /api/event/:id updates an existing event', (done) => {

		request(app)
			.put('/api/event/8')
			.set('token', token)
			.expect(200)
			.send({usernames: [{username: 'testinguser5'}]})
			.then((response) => {
				assert(response.body.usernames[0].username === 'testinguser5');
				done();
			});

	});

	it('a DELETE request to /api/event/:id deletes an existing event', (done) => {

		request(app)
			.delete('/api/event/8')
			.set('token', token)
			.expect(204)
			.then(() => done());
	});

	it('a POST request to /api/event/:id/join updates or creates an event', (done) => {

		request(app)
			.post('/api/event/8/join')
			.set('token', token)
			.send({ usernames: [{ username: 'testinguser6' }] })
			.expect(200)
			.then((response) => {
				assert(response.body.usernames[0].username === 'testinguser6');
				assert(response.body.eventId === 8);
				return request(app)
					.post('/api/event/30/join')
					.set('token', token)
					.send({ usernames: [{ username: 'testinguser7' }] })
					.expect(200);
			})
			.then((response) => {
				assert(response.body.usernames[0].username === 'testinguser7');
				assert(response.body.eventId === 30);
				done();
			});

	});

});
