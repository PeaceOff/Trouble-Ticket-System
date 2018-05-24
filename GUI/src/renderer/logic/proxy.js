require('dotenv').config()
const _axios = require('axios')

const endpoint = process.env.API_ENDPOINT || 'http://localhost:63770'

async function answerSecondaryQuestion (id, answer) {
  try {
    var response = await _axios.put(`${endpoint}/api/SecondaryTickets/${id}`, {
      text: answer
    })
    console.log(response)
    return true
  } catch (error) {
    console.log(error)
    return false
  }
}

export default {
  answerSecondaryQuestion
}
