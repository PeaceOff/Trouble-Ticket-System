require('dotenv').config()
const amqp = require('amqplib/callback_api')
const jsonfile = require('jsonfile')
const dept = process.env.DEPARTMENT_NAME || 'hello'
const msgFile = `./${dept}.json`
let tickets = []

// Fetching stored data
try {
  tickets = jsonfile.readFileSync(msgFile)
} catch (err) {
  console.log('No file found. Assuming empty array')
}

// Connecting to the Message Queue
amqp.connect('amqp://localhost', function (err, conn) {
  if (err) {
    console.log(err)
    return
  }
  conn.createChannel(function (err, ch) {
    if (err) {
      console.log(err)
      return
    }

    ch.assertQueue(dept, {durable: false})
    console.log(' [*] Waiting for messages in %s. To exit press CTRL+C', dept)
    ch.consume(dept, onMessageReceived, {noAck: true})
  })
})

// Handle new messages
function onMessageReceived (msg) {
  console.log(' [x] Received %s', msg.content.toString())

  let nTicket = JSON.parse(msg.content.toString())
  // {id: ++_id, title: 'bom dia alegria', description: 'esta descrição é muito boa.'}
  tickets.push(nTicket)
  storeData()
}

// Store tickets in local file system
function storeData () {
  jsonfile.writeFile(msgFile, tickets, (err) => {
    if (err) { console.log(err) }
  })
}

function setTickets (t) {
  tickets = t
  storeData()
}

module.exports = {
  dept,
  tickets,
  setTickets
}
