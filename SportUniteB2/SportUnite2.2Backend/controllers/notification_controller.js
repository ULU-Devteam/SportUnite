const neo = require('../databases/neo.js').driver;

module.exports = {

	postEventNotifications(req, res, next) {
		const session = neo.session();
		console.log(req.body.friends);

		session.run(
			"MATCH (user:User {username: {username}}) " +
			"MATCH (friend:User) " +
			"WHERE friend.username IN {friends} " +
			"MERGE (user)-[rel:NOTIFIES {eventId: {eventId}, read: {read}, message: {message}, sender: {username}}]->(friend) " +
			"RETURN rel.eventId, rel.read, rel.message, rel.sender",
			{
				username: req.body.username,
				friends: req.body.friends,
				eventId: req.params.id,
				read: req.body.read,
				message: req.body.message
			})
			.then((result) => {
				console.log(result);
				session.close();
				if (result.records.length > 0) {
					const rec = result.records[0];
					res.status(200).json({
						eventId: rec.get('rel.eventId'),
						read: rec.get('rel.read'),
						message: rec.get('rel.message'),
						sender: rec.get('rel.sender')
					});
				} else {
					res.status(404).json(result.records);
				}
			})
			.catch((error) => {
				session.close();
				console.error(error);
				res.status(400).json(error);
			});
	},

	updateNotification(req, res, next) {
		const session = neo.session();

		session.run(
			"MATCH (user:User {username: {username}})<-[rel:NOTIFIES {eventId: {eventId}}]-() " +
			"SET rel.read = {read} " +
			"RETURN rel.eventId, rel.read, rel.message, rel.sender",
			{
				username: req.params.username,
				eventId: req.params.id,
				read: req.body.read
			})
			.then((result) => {
				session.close();
				if (result.records.length > 0) {
					const rec = result.records[0];
					res.status(200).json({
						eventId: rec.get('rel.eventId'),
						read: rec.get('rel.read'),
						message: rec.get('rel.message'),
						sender: rec.get('rel.sender')
					});
				} else {
					res.status(404).json(result.records);
				}
			})
			.catch((error) => {
				session.close();
				console.error(error);
				res.status(400).json(error);
			});
	},

	updateAllNotifications(req, res, next) {
		const session = neo.session();

		session.run(
			"MATCH (user:User {username: {username}})<-[rel:NOTIFIES]-() " +
			"SET rel.read = {read} " +
			"RETURN rel.eventId, rel.read, rel.message, rel.sender",
			{
				username: req.params.username,
				eventId: req.params.id,
				read: req.body.read
			})
			.then((result) => {
				session.close();
				if (result.records.length > 0) {
					const rec = result.records[0];
					res.status(200).json({
						eventId: rec.get('rel.eventId'),
						read: rec.get('rel.read'),
						message: rec.get('rel.message'),
						sender: rec.get('rel.sender')
					});
				} else {
					res.status(404).json(result.records);
				}
			})
			.catch((error) => {
				session.close();
				console.error(error);
				res.status(400).json(error);
			});
	},

	getUserNotifications(req, res, next) {
		const session = neo.session();

		session.run(
			"MATCH (user:User {username: {username}})<-[rel:NOTIFIES]-() " +
			"OPTIONAL MATCH (user:User {username: {username}})<-[unread:NOTIFIES {read: false}]-() " +
			"RETURN rel.eventId, rel.read, rel.message, rel.sender, count(unread) AS unread",
			{
				username: req.params.username
			})
			.then((result) => {
				session.close();
				let response = [];
				for (let rec of result.records) {
					response.push({
						eventId: rec.get('rel.eventId'),
						read: rec.get('rel.read'),
						message: rec.get('rel.message'),
						sender: rec.get('rel.sender')
					});
				}
				res.status(200).json({unread: result.records[0].get('unread').toNumber(), notifications: response});
			})
			.catch((error) => {
				session.close();
				res.status(400).json(error);
			});
	}
};
