const neo = require('../databases/neo.js').driver;

module.exports = {

	post(req, res, next) {
		const session = neo.session();

		session.run(
			"MATCH (user:User {username: {username}}) " +
			"MATCH (friend:User {username: {friendname}}) " +
			"OPTIONAL MATCH (friend)-[:LIVES_IN]->(city) " +
			"MERGE (user)-[:FRIENDS_WITH]->(friend) " +
			"RETURN friend.name, friend.username, friend.age, friend.email, city.name",
			{
				username: req.params.username,
				friendname: req.body.username
			})
			.then((result) => {
				session.close();
				if (result.records.length > 0) {
					const rec = result.records[0];
					res.status(200).json({
						username: rec.get('friend.username'),
						name: rec.get('friend.name'),
						age: rec.get('friend.age'),
						email: rec.get('friend.email'),
						city: rec.get('city.name')
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

	get(req, res, next) {
		const session = neo.session();

		session.run(
			"MATCH (user:User {username: {username}})-[:FRIENDS_WITH]-(friend:User) " +
			"OPTIONAL MATCH (friend)-[:LIVES_IN]->(city) " +
			"RETURN friend.name, friend.username, friend.age, friend.email, city.name",
			{
				username: req.params.username
			})
			.then((result) => {
				session.close();
				let response = [];
				for (let rec of result.records) {
					response.push({
						username: rec.get('friend.username'),
						name: rec.get('friend.name'),
						age: rec.get('friend.age'),
						email: rec.get('friend.email'),
						city: rec.get('city.name')
					});
				}
				res.status(200).json(response);
			})
			.catch((error) => {
				session.close();
				console.error(error);
				res.status(400).json(error);
			});
	},

	deleteOne(req, res, next) {
		const session = neo.session();

		session.run(
			"MATCH (user:User {username: {username}}) " +
			"MATCH (friend:User {username: {friendname}})-[rel:FRIENDS_WITH]-(user) " +
			"OPTIONAL MATCH (friend)-[:LIVES_IN]->(city) " +
			"DELETE rel " +
			"RETURN friend.name, friend.username, friend.age, friend.email, city.name",
			{
				username: req.params.username,
				friendname: req.params.friendname
			})
			.then((result) => {
				session.close();
				if (result.records.length > 0) {
					const rec = result.records[0];
					res.status(200).json({
						username: rec.get('friend.username'),
						name: rec.get('friend.name'),
						age: rec.get('friend.age'),
						email: rec.get('friend.email'),
						city: rec.get('city.name')
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
	}
};
