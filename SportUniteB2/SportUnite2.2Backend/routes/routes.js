const AccountController = require('../controllers/account_controller');
const FriendsController = require('../controllers/friends_controller');
const EventController = require('../controllers/event_controller');
const NotificationController = require('../controllers/notification_controller');
const jwt = require('jsonwebtoken');

module.exports = (app) => {

	// Login route
    app.post('/api/account/login', AccountController.login);
    app.post('/api/account', AccountController.post);
    app.post('/api/account/googlelogin', AccountController.googleLogin);
    app.post('/api/account/googleregister', AccountController.googleRegister);
    app.post('/api/account/facebooklogin', AccountController.facebookLogin);
    app.post('/api/account/facebookregister', AccountController.facebookRegister);

    app.all('*', AccountController.tokenCheck);

    // Account routes
	app.get('/api/account', AccountController.get);
	app.get('/api/account/:username', AccountController.getOne);
	app.put('/api/account/:username', AccountController.put);
	app.delete('/api/account/:username', AccountController.deleteOne);


	// Friends routes
	app.get('/api/account/:username/friends', FriendsController.get);
	app.post('/api/account/:username/friends', FriendsController.post);
	app.delete('/api/account/:username/friends/:friendname', FriendsController.deleteOne);

	//Notification routes
	app.get('/api/account/:username/notifications', NotificationController.getUserNotifications);
	app.put('/api/account/:username/notifications/:id', NotificationController.updateNotification);
    app.put('/api/account/:username/notifications', NotificationController.updateAllNotifications);
	app.post('/api/event/:id/notifications', NotificationController.postEventNotifications);

	// Event routes

	app.get('/api/event', EventController.get);
	app.get('/api/event/:id', EventController.getOne);

	app.post('/api/event', EventController.post);
	app.put('/api/event/:id', EventController.put);
	app.delete('/api/event/:id', EventController.delete);
	app.post('/api/event/:id/join', EventController.join);


    app.get('/api/event/:id/users', EventController.getEventUsers);
    app.post('/api/event/:id/mail', EventController.sendMail);

};
