require('dotenv').config()
const axios = require('axios')
const endpoint = process.env.API_ENDPOINT || 'http://localhost:56722'

function answerSecondaryQuestion (id, answer) {
  axios.put(`${endpoint}/api/SecondaryTickets/${id}`, {
    id: id,
    answer: answer
  }).then(function (response) {
    console.log(response)
  }).catch(function (error) {
    console.log(error)
  })
}

module.exports = {
  answerSecondaryQuestion
}
