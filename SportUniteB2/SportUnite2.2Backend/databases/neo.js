const neo4j = require('neo4j-driver').v1;
const env = require('../config/environment');

const driver = neo4j.driver(env.neoConnectionUrl,
	neo4j.auth.basic(
		env.neoUser,
		env.neoPassword
	));

driver.onCompleted = () => {
	console.log('Neo4j driver created');
};

driver.onError = error => {
	console.log(error);
};

process.on('exit', () => driver.close());

function createConstraints() {
	const session = driver.session();

	const promises = [
		session.run("CREATE CONSTRAINT ON (user:User) ASSERT user.username IS UNIQUE"),
		// Property existence constraint requires Neo4j Enterprise Edition
		// session.run("CREATE CONSTRAINT ON (u:User) ASSERT exists(u.username)")
	];

	Promise.all(promises)
		.then(() => {
			session.close();
		})
		.catch((error) => {
			session.close();
			console.error(error);
		});

}

module.exports = {driver, createConstraints};
