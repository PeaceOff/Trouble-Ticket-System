const amqp = require('amqplib/callback_api')
const jsonfile = require('jsonfile')
const dept = 'genius'
const msgFile = `./data/${dept}.json`
var tickets

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

  storeData()
}

// Load tickets from local file system
function loadData () {
  console.log('Loading data')

  jsonfile.readFile(msgFile, (err, obj) => {
    if (err) {
      console.log(err)
      return
    }

    tickets = obj
  })
}

// Store tickets in local file system
function storeData () {
  jsonfile.writeFile(msgFile, tickets, (err) => {
    if (err) { console.log(err) }
  })
}

// Loading data on start
loadData()

module.exports = {
  dept,
  tickets
}
