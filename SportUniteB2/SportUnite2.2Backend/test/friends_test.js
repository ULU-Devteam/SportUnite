const chai = require('chai');
const assert = chai.assert;
const request = require('supertest');
const app = require('../app');
const token = require('./test_helper');

describe('Account test', () => {

	let user, user2;

	function assertUser(body, expected) {
		assert(body.username === expected.username);
		assert(body.name === expected.name);
		assert(body.age === expected.age);
		assert(body.email === expected.email);
		assert(body.city === expected.city);
	}

	beforeEach((done) => {

		user = {
			username: "test",
			password: "test123",
			name: "test",
			age: 1,
			email: "test@test.com",
			city: "city"
		};

		user2 = {
			username: "test2",
			password: "test123",
			name: "test2",
			age: 2,
			email: "test2@test.com",
			city: "city2"
		};

		const promises = [
			request(app).post('/api/account').send(user),
			request(app).post('/api/account').send(user2)
		];

		Promise.all(promises)
			.then((result) => {
				user = result[0].body;
				user2 = result[1].body;
				done();
			})
			.catch((error) => done(error));
	});

	it('POST request at /api/account/:username/friends returns the added friend', (done) => {
		request(app)
			.post('/api/account/' + user.username + '/friends')
			.set({token: token, Accept: 'application/json'})
			.send(user2)
			.expect('Content-Type', /json/)
			.expect(200)
			.then((result) => {
				const body = result.body;
				assert.isNotArray(body);
				assertUser(body, user2);
				done();
			})
			.catch((error) => done(error));
	});

	it('GET request at /api/account/:username/friends returns a list of the users friends', (done) => {
		request(app)
			.post('/api/account/' + user.username + '/friends')
			.set({token: token, Accept: 'application/json'})
			.send(user2)
			.expect('Content-Type', /json/)
			.expect(200)
			.then(() => {
				return request(app)
					.get('/api/account/' + user.username + '/friends')
					.set({token: token, Accept: 'application/json'})
					.expect('Content-Type', /json/)
					.expect(200)
			})
			.then((result) => {
				const body = result.body;
				assert.isArray(body);
				assert.lengthOf(body, 1);
				assertUser(body[0], user2);
				done();
			})
			.catch((error) => done(error));
	});

	it('DELETE request at /api/account/:username/friends/:username unfriends and returns the unfriended friend', (done) => {
		request(app)
			.post('/api/account/' + user.username + '/friends')
			.set({token: token, Accept: 'application/json'})
			.send(user2)
			.expect('Content-Type', /json/)
			.expect(200)
			.then(() => {
				return request(app)
					.delete('/api/account/' + user.username + '/friends/' + user2.username)
					.set({token: token, Accept: 'application/json'})
					.expect('Content-Type', /json/)
					.expect(200)
			})
			.then((result) => {
				const body = result.body;
				assert.isNotArray(body);
				assertUser(body, user2);
				return request(app)
					.delete('/api/account/' + user.username + '/friends/' + user2.username)
					.set({token: token, Accept: 'application/json'})
					.expect('Content-Type', /json/)
					.expect(404)
			})
			.then(() => done())
			.catch((error) => done(error));
	});

});
