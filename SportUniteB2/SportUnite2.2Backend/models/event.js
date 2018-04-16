const mongoose = require('mongoose');
const Schema = mongoose.Schema;
const UserSchema = require('./user');

const EventSchema = new Schema({
	eventId: {
		type: Number,
		required: [true, 'An eventID is required.']
	},
	date: Date,
	sportcomplex: Object,
    long: Number,
    lat: Number,

	usernames: [UserSchema]
});

const Event = mongoose.model('event', EventSchema);

module.exports = Event;