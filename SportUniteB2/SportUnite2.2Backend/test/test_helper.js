const mongoose = require('mongoose');
const neo = require('../databases/neo').driver;
const jwt = require('jsonwebtoken');
const config = require('../config/environment');

mongoose.Promise = global.Promise;
const token = jwt.sign({data: "admin"}, config.environment.SECRET_KEY, {expiresIn: '15m'});

before((done) => {
	mongoose.connect('mongodb://test:test@ds062807.mlab.com:62807/sportunitetestmongodb', {useMongoClient: true});
	mongoose.connection
		.once('open', () => {
			console.log('Connected to MongoDB testing database on test mlab db\n');
			done();
		})
		.on('error', (error) => {
			console.warn('Warning', error.toString());
		});
});

beforeEach((done) => {
	const {events} = mongoose.connection.collections;
	const session = neo.session();

	const promises = [
		session.run("MATCH (n) DETACH DELETE n"),
		events.drop
	];

	Promise.all(promises)
		.then(() => {
			session.close();
			done();
		})
		.catch((error) => {
			session.close();
			done(error);
		});
});

module.exports = token;
