var api_key = 'key-a0bd196887e3fb23868686061c185c02';
var domain = 'sandbox360598fdffde45ae8a8f83588cc7d903.mailgun.org';
var mailgun = require('mailgun-js')({apiKey: api_key, domain: domain});



const mail =  function (subject, to, body) {
    var data = {
        from: 'SprotUntie <sportunite@sportunite.com>',
        to: to,
        subject: subject,
        text: body
    };

    mailgun.messages().send(data, function (error, body) {
         return body;
    });
}

module.exports = mail;