import Vue from 'vue'
import Axios from 'axios'

Axios.defaults.headers.common.Accept = 'application/json'
Axios.defaults.baseURL = process.env.API_ENDPOINT || 'http://localhost:51568'
Axios.interceptors.response.use(
  response => response,
  (error) => {
    return Promise.reject(error)
  })

// Bind Axios to Vue.
Vue.$http = Axios
Object.defineProperty(Vue.prototype, '$http', {
  get () {
    return Axios
  }
})
