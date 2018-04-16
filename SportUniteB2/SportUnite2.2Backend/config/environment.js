const environment = {
	webPort: process.env.PORT || 3000,
	dbHost: process.env.DB_HOST || 'localhost',
	dbPort: process.env.DB_PORT || '7687',
	dbUser: process.env.DB_USER || 'Admin',
	dbPassword: process.env.DB_PASSWORD || 'Admin123',
	dbDatabase: process.env.DB_DATABASE || 'SportUniteEvents',
	SECRET_KEY: 'serectkey'
};

const google_client_id = '658702919219-1fpbt81i71q88mkhv9phslmi213fah6s.apps.googleusercontent.com';
const facebook_client_id = '328216757674961';
const facebook_client_special = '8e8258cb4f5c8f6801f5d3b0e3e39953';

let neoConnectionUrl;
let neoUser;
let neoPassword;
let test = false;


console.log(process.argv.forEach((val, index, array) => {
	if(val === 'test'){
		test = true;
	};

}));
if(test){
    neoConnectionUrl =  'bolt://hobby-jhgalbmipdbcgbkeogpaajal.dbs.graphenedb.com:24786';
    neoUser = 'Admin';
    neoPassword = 'b.lZXv1QREMsYu.rYbY7ZsbEX1bWvgJ';
}else if(process.env.NODE_ENV === 'production') {
    neoConnectionUrl =  'bolt://hobby-kodbceonlgkmgbkepfaecjal.dbs.graphenedb.com:24786'
    neoUser = 'Admin';
    neoPassword = 'b.7VkhjFTXsEt1.7Qbjh4UTf4gps6fj';
}else{
    // neoConnectionUrl =  'bolt://' + environment.dbHost + ':' + environment.dbPort
    // neoUser = 'Admin';
    // neoPassword = 'Admin';
    neoConnectionUrl =  'bolt://hobby-kodbceonlgkmgbkepfaecjal.dbs.graphenedb.com:24786'
    neoUser = 'Admin';
    neoPassword = 'b.7VkhjFTXsEt1.7Qbjh4UTf4gps6fj';
}

// const mongoConnectionUrl = process.env.NODE_ENV === 'production' ?
// 	'mongodb://Admin:admin@ds062797.mlab.com:62797/sportunitemongodb' :
// 	'mongodb://localhost/' + environment.dbDatabase;

const mongoConnectionUrl =
    'mongodb://Admin:admin@ds062797.mlab.com:62797/sportunitemongodb';

module.exports = {
	environment: environment,
	neoConnectionUrl: neoConnectionUrl,
	neoUser: neoUser,
	neoPassword: neoPassword,
	mongoConnectionUrl: mongoConnectionUrl,
	google_client_id: google_client_id,
	facebook_client_id: facebook_client_id,
	facebook_client_special: facebook_client_special
};
