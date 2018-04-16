const chai = require('chai');
const assert = chai.assert;
const request = require('supertest');
const app = require('../app');
const token = require('./test_helper');

describe('Account test', () => {
	let user;

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

		request(app).post('/api/account')
			.send(user)
			.then((result) => {
				user = result.body;
				done();
			})
			.catch((error) => done(error));
	});

	it('login', (done) => {
		request(app).post('/api/account/login').send(
			{
				username: "test",
				password: "test123"
			}
		).then((result) => {
			assert(result.body.authorized === true);
			assert(result.body.token);
			done();
		})
	});

	it('GET request at /api/account returns a list of all users', (done) => {
		request(app)
			.get('/api/account')
			.set({token: token, Accept: 'application/json'})
			.expect('Content-Type', /json/)
			.expect(200)
			.then((result) => {
				const body = result.body;
				assert.isArray(body);
				assert.lengthOf(body, 1);
				assertUser(body[0], user);
				done();
			})
			.catch((error) => {
				done(error);
			});
	});

	it('GET request at /api/account/:username returns a single user', (done) => {
		request(app)
			.get('/api/account/' + user.username)
			.set({token: token, Accept: 'application/json'})
			.expect('Content-Type', /json/)
			.expect(200)
			.then((result) => {
				const body = result.body;
				assert.isNotArray(body);
				assertUser(body, user);
				done();
			});
	});

	it('POST request at /api/account returns the newly created user or an error if the username already exists', (done) => {
		const newuser = {
			username: "new",
			password: "new123",
			name: "new",
			age: 2,
			email: "new@new.com",
			city: "New York"
		};

		request(app)
			.post('/api/account')
			.set({Accept: 'application/json'})
			.send(newuser)
			.expect(201)
			.then((result) => {
				const body = result.body;
				assert.isNotArray(body);
				assertUser(body, newuser);
				return request(app)
					.post('/api/account')
					.send(user)
					.expect(400);
			})
			.then((result) => {
				assert(result.body.error);
				done();
			})
			.catch((error) => {
				done(error);
			});
	});

	xit('PUT request at api/account/:username returns the updated user', (done) => {
		const updateduser = {
			username: "test",
			name: "updated",
			age: 10,
			email: "updated@updated.com",
			city: "Updated York"
		};

		request(app)
			.put('/api/account/' + user.username)
			.set({token: token, Accept: 'application/json'})
			.send(updateduser)
			.expect(200)
			.then((result) => {
				const body = result.body;
				assert.isNotArray(body);
				assertUser(body, updateduser);
				done();
			})
			.catch((error) => {
				done(error);
			});
	});

	it('DELETE request at api/account/:username returns the deleted user', (done) => {
		request(app)
			.delete('/api/account/' + user.username)
			.set({token: token, Accept: 'application/json'})
			.expect('Content-Type', /json/)
			.expect(200)
			.then((result) => {
				const body = result.body;
				assert.isNotArray(body);
				assertUser(body, user);
				return request(app)
					.get('/api/account/' + user.username)
					.set({token: token, Accept: 'application/json'})
					.expect('Content-Type', /json/)
					.expect(404);
			})
			.then(() => done())
			.catch((error) => {
				done(error);
			});
	});

});
