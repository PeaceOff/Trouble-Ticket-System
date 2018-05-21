require('dotenv').config()
const vue = require('vue')

const endpoint = process.env.API_ENDPOINT || 'http://localhost:51568'

function answerSecondaryQuestion (id, answer) {
  vue.$http.put(`${endpoint}/api/SecondaryTickets/${id}`, {
    id: id,
    answer: answer
  }).then(function (response) {
    console.log(response)
  }).catch(function (error) {
    console.log(error)
  })
}

function login (data) {
  console.log('Login com ' + data.username)
  /* const requestType = 'post'
  vue.$http[requestType](`${endpoint}/api/Account/Login`, {
    username: data.username,
    password: data.password
  }).then(function (response) {
    console.log('Proxy response:')
    console.log(response)
  }).catch(function (error) {
    console.log('Proxy error:')
    console.log(error)
  })
  return new Promise((resolve, reject) => {
    vue.$http[requestType](`${endpoint}/api/Account/Login`, {
      username: data.username,
      password: data.password
    }).then((response) => {
        resolve(response.data);
      })
      .catch(({ response }) => {
        if (response) {
          reject(response.data);
        } else {
          reject({
            success: false,
            message: 'Unexpected error. We\'re sorry for the inconvenience.',
          });
        }
      });
  }); */
}

module.exports = {
  answerSecondaryQuestion,
  login
}
