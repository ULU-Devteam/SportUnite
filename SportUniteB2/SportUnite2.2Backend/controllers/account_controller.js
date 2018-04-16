const neo = require('../databases/neo.js').driver;
const bcrypt = require('bcryptjs');
const jwt = require('jsonwebtoken');
const https = require('https');
const config = require('../config/environment');
const verifier = require('google-id-token-verifier');


module.exports = {

	tokenCheck(req, res, next) {
		let tokenValid = false;
		let googleValid = false;
		let facebookValid = false;
		const token = req.body.token || req.headers.token;
		if (token) {
			new Promise((resolve, reject) => {
				https.get('https://graph.facebook.com/debug_token?input_token=' + token + '&access_token=' +
					config.facebook_client_id + '|' + config.facebook_client_special, (response) => {
					let resolvedResponse = '';
					let parsedResponse = '';

					response.on('data', (data) => {
						resolvedResponse += data;
					});

					response.on('end', () => {
						parsedResponse = JSON.parse(resolvedResponse);
						if (parsedResponse.data.is_valid === true) {
							facebookValid = true;
							resolve();
						} else {
							facebookValid = false;
							reject();
						}
					});
				}).on('error', (error) => {
					facebookValid = false;
					console.log(error);
				})
			}).then(() => {
				console.log('facebook verified');
				next();
			}).catch(() => {
				jwt.verify(token, config.environment.SECRET_KEY, (err, decode) => {
					if (err) {
						tokenValid = false;
						verifier.verify(token, config.google_client_id, function (err, tokenInfo) {
								if (!err) {
									console.log('google verified');
									googleValid = true;
								} else {
									googleValid = false;
								}
							}
						);
					} else {
						console.log('JWT verified');
						tokenValid = true;
					}
				});

				if (googleValid || tokenValid || facebookValid) {
					next();
				} else {
					res.status(401).json({error: 'token not valid'});
				}
			});
		} else {
			res.status(401).json({error: 'please supply a token'});
		}
	},

	get(req, res, next) {
		const session = neo.session();

		session.run(
			"MATCH (user:User) " +
			"OPTIONAL MATCH (user)-[:LIVES_IN]->(city) " +
			"RETURN user.name, user.username, user.age, user.email, city.name")
			.then((result) => {
				session.close();
				let response = [];
				for (let rec of result.records) {
					response.push({
						username: rec.get('user.username'),
						name: rec.get('user.name'),
						age: rec.get('user.age'),
						email: rec.get('user.email'),
						city: rec.get('city.name')
					});
				}
				res.status(200).json(response);
			})
			.catch((error) => {
				session.close();
				res.status(400).json(error);
			});
	},

	getOne(req, res, next) {
		const session = neo.session();

		session.run(
			"MATCH (user:User {username: {username}}) " +
			"OPTIONAL MATCH (user)-[:LIVES_IN]->(city) " +
			"RETURN user.name, user.username, user.age, user.email, city.name, user.radius",
			{
				username: req.params.username
			})
			.then((result) => {
				session.close();
				if (result.records.length > 0) {
					const rec = result.records[0];
					res.status(200).json({
						username: rec.get('user.username'),
						name: rec.get('user.name'),
						age: rec.get('user.age'),
						email: rec.get('user.email'),
						city: rec.get('city.name'),
						radius: rec.get('user.radius')
					});
				} else {
					res.status(404).json(result.records);
				}
			})
			.catch((error) => {
				session.close();
				res.status(400).json(error);
			});
	},

	post(req, res, next) {
		const session = neo.session();

		const saltRounds = 10;
		const password = req.body.password;

		bcrypt.genSalt(saltRounds, (err, salt) => {
			bcrypt.hash(password, salt, (err, hash) => {

				session.run(
					"MERGE (user:User {username: {userUsername}, name: {userName}, age: {userAge}, email: {userEmail}, radius: {radius}, " +
					"createdOn: timestamp(), password: {password}}) " +
					"MERGE (city:City {name: {cityName}}) " +
					"MERGE (user)-[:LIVES_IN]->(city) " +
					"RETURN user.name, user.username, user.age, user.email, city.name, user.radius",
					{
						userUsername: req.body.username,
						userName: req.body.name,
						userAge: req.body.age,
						userEmail: req.body.email,
						cityName: req.body.city,
						password: hash,
						radius: 10
					})
					.then((result) => {
						session.close();
						const rec = result.records[0];
						res.status(201).json({
							username: rec.get('user.username'),
							name: rec.get('user.name'),
							age: rec.get('user.age'),
							email: rec.get('user.email'),
							city: rec.get('city.name'),
							radius: rec.get('user.radius')
						});
					})
					.catch((error) => {
						console.log(error.message);
						session.close();
						res.status(400).json({error: error.message});
					});
			});
		});
	},

	put(req, res, next) {
		const session = neo.session();

		session.run(
			"MATCH (user:User {username: {userUsername}})-[rel:LIVES_IN]->() " +
			"DELETE rel " +
			"SET user.username = {userUsername}, user.name = {userName}, user.age = {userAge}, user.email = {userEmail}, user.radius = {radius}, " +
			"user.lastUpdatedOn = timestamp() " +
			"MERGE (city:City {name: {cityName}}) " +
			"CREATE (user)-[:LIVES_IN]->(city) " +
			"RETURN user.name, user.username, user.age, user.email, city.name, user.radius",
			{
				userUsername: req.params.username,
				userName: req.body.name,
				userAge: req.body.age,
				userEmail: req.body.email,
				cityName: req.body.city,
				radius: req.body.radius
			})
			.then((result) => {
				session.close();
				if (result.records.length > 0) {
					const rec = result.records[0];
					res.status(200).json({
						username: rec.get('user.username'),
						name: rec.get('user.name'),
						age: rec.get('user.age'),
						email: rec.get('user.email'),
						city: rec.get('city.name'),
						radius: rec.get('user.radius')
					});
				} else {
					res.status(404).json(result.records);
				}
			})
			.catch((error) => {
				session.close();
				res.status(400).json(error);
			});
	},

	deleteOne(req, res, next) {
		const session = neo.session();

		session.run(
			"MATCH (user:User {username: {userUsername}}) " +
			"OPTIONAL MATCH (user)-[:LIVES_IN]->(city:City) " +
			"WITH user, city, user.username as username, user.name as name, user.age as age, user.email as email " +
			"DETACH DELETE user " +
			"RETURN username, name, age, email, city.name",
			{
				userUsername: req.params.username
			})
			.then((result) => {
				session.close();
				if (result.records.length > 0) {
					const rec = result.records[0];
					res.status(200).json({
						username: rec.get('username'),
						name: rec.get('name'),
						age: rec.get('age'),
						email: rec.get('email'),
						city: rec.get('city.name')
					});
				} else {
					res.status(404).json(result.records);
				}
			})
			.catch((error) => {
				session.close();
				res.status(400).json(error);
			});
	},

	login(req, res, next) {

		const username = req.body.username;
		const password = req.body.password;
		const session = neo.session();

		session.run(
			"match (n:User{username: $username})" +
			"return n.password",
			{username: username})
			.then((result) => {
				console.log(result);
				session.close();
				if (result.records[0] !== undefined) {
					bcrypt.compare(password, result.records[0].get('n.password'), function (err, resLogin) {
						if (resLogin) {
							const token = jwt.sign({data: username}, config.environment.SECRET_KEY, {expiresIn: '12h'});
							res.status(200).json({
								authorized: true,
								token: token,
								user: username
							})
						} else {
							res.status(401).json({authorized: false});
						}
					});
				} else {
					res.status(401).json({authorized: false});
				}
			})
			.catch(() => {
				session.close();
				next();
			});
	},

	googleLogin(req, res, next) {
		const session = neo.session();
		const token = req.body.token || req.headers.token;
		if (token) {
			verifier.verify(token, config.google_client_id, function (err, tokenInfo) {
				if (!err) {
					session.run(
						"MATCH (user:User {userGoogleID: {userGoogleID}}) " +
						"OPTIONAL MATCH (user)-[:LIVES_IN]->(city) " +
						"RETURN user.name, user.username, user.age, user.email, city.name",
						{
							userGoogleID: tokenInfo.sub + 'google'
						})
						.then((result) => {
							session.close();
							if (result.records.length > 0) {
								const rec = result.records[0];
								res.status(200).json({
									username: rec.get('user.username'),
									name: rec.get('user.name'),
									age: rec.get('user.age'),
									email: rec.get('user.email'),
									city: rec.get('city.name')
								});
							} else {
								res.status(200).json(result.records);
							}
						})
						.catch((error) => {
							session.close();
							res.status(400).json(error);
						});
				} else {
					res.status(401).json({error: 'Invalid Google token'});
				}
			});
		} else {
			res.status(401).json({error: 'Google ID token required for login'});
		}
	},

	googleRegister(req, res, next) {
		const session = neo.session();
		const token = req.body.token || req.headers.token;
		if (token) {
			verifier.verify(token, config.google_client_id, function (err, tokenInfo) {
				if (!err) {

					session.run(
						"MERGE (user:User {userGoogleID: {userGoogleID}, username: {userUsername}, name: {userName}, age: {userAge}, email: {userEmail}, " +
						"createdOn: timestamp()}) " +
						"MERGE (city:City {name: {cityName}}) " +
						"MERGE (user)-[:LIVES_IN]->(city) " +
						"RETURN user.name, user.username, user.age, user.email, city.name",
						{
							userGoogleID: tokenInfo.sub + 'google',
							userUsername: req.body.username,
							userName: req.body.name,
							userAge: req.body.age,
							userEmail: req.body.email,
							cityName: req.body.city
						})
						.then((result) => {
							session.close();
							const rec = result.records[0];
							res.status(201).json({
								username: rec.get('user.username'),
								name: rec.get('user.name'),
								age: rec.get('user.age'),
								email: rec.get('user.email'),
								city: rec.get('city.name')
							});
						})
						.catch((error) => {
							session.close();
							res.status(400).json({error: error.message});
						});
				} else {
					res.status(401).json({error: 'Invalid Google token'});
				}
			})
		} else {
			res.status(401).json({error: 'Google ID token required for registration'});
		}
	},

	facebookLogin(req, res, next) {
		const session = neo.session();
		const token = req.body.token || req.headers.token;
		if (token) {
			new Promise((resolve, reject) => {
				https.get('https://graph.facebook.com/debug_token?input_token=' + token + '&access_token=' +
					config.facebook_client_id + '|' + config.facebook_client_special, (response) => {
					let resolvedResponse = '';
					let parsedResponse = '';

					response.on('data', (data) => {
						resolvedResponse += data;
					});

					response.on('end', () => {
						parsedResponse = JSON.parse(resolvedResponse);
						if (parsedResponse.data.is_valid === true) {
							resolve(parsedResponse);
						} else {
							reject();
						}
					});
				}).on('error', (error) => {
					console.log(error);
				});
			}).then((parsedResponse) => {
				session.run(
					"MATCH (user:User {userFacebookID: {userFacebookID}}) " +
					"OPTIONAL MATCH (user)-[:LIVES_IN]->(city) " +
					"RETURN user.name, user.username, user.age, user.email, city.name",
					{
						userFacebookID: parsedResponse.data.user_id + 'facebook'
					})
					.then((result) => {
						session.close();
						if (result.records.length > 0) {
							const rec = result.records[0];
							res.status(200).json({
								username: rec.get('user.username'),
								name: rec.get('user.name'),
								age: rec.get('user.age'),
								email: rec.get('user.email'),
								city: rec.get('city.name')
							});
						} else {
							res.status(200).json(result.records);
						}
					})
					.catch((error) => {
						session.close();
						res.status(400).json(error);
					});
			}).catch(() => {
				res.status(401).json({error: 'Facebook ID token is not valid'});
			})
		} else {
			res.status(401).json({error: 'Facebook ID token required for login'});
		}
	},

	facebookRegister(req, res, next) {
		const session = neo.session();
		const token = req.body.token || req.headers.token;
		if (token) {
			new Promise((resolve, reject) => {
				https.get('https://graph.facebook.com/debug_token?input_token=' + token + '&access_token=' +
					config.facebook_client_id + '|' + config.facebook_client_special, (response) => {
					let resolvedResponse = '';
					let parsedResponse = '';

					response.on('data', (data) => {
						resolvedResponse += data;
					});

					response.on('end', () => {
						parsedResponse = JSON.parse(resolvedResponse);
						if (parsedResponse.data.is_valid === true) {
							resolve(parsedResponse);
						} else {
							reject();
						}
					});
				}).on('error', (error) => {
					console.log(error);
				});
			}).then((parsedResponse) => {
				session.run(
					"MERGE (user:User {userFacebookID: {userFacebookID}, username: {userUsername}, name: {userName}, age: {userAge}, email: {userEmail}, " +
					"createdOn: timestamp()}) " +
					"MERGE (city:City {name: {cityName}}) " +
					"MERGE (user)-[:LIVES_IN]->(city) " +
					"RETURN user.name, user.username, user.age, user.email, city.name",
					{
						userFacebookID: parsedResponse.data.user_id + 'facebook',
						userUsername: req.body.username,
						userName: req.body.name,
						userAge: req.body.age,
						userEmail: req.body.email,
						cityName: req.body.city
					})
					.then((result) => {
						session.close();
						const rec = result.records[0];
						res.status(201).json({
							username: rec.get('user.username'),
							name: rec.get('user.name'),
							age: rec.get('user.age'),
							email: rec.get('user.email'),
							city: rec.get('city.name')
						});
					})
					.catch((error) => {
						console.log(error.message);
						session.close();
						res.status(400).json({error: error.message});
					});
			}).catch(() => {
				res.status(401).json({error: 'Facebook ID token is not valid'});
			});

		} else {
			res.status(401).json({error: 'Facebook ID token required for registration'});
		}

	}
};
