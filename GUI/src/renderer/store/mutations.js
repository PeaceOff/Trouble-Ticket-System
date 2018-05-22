import Vue from 'vue'
import {
  CHECK_LOGIN,
  LOGIN_SUCCESS,
  LOGOUT
} from './mutations-types'

export default {
  [CHECK_LOGIN] (state) {
    state.isLoggedIn = !!localStorage.getItem('token')
    if (state.isLoggedIn) {
      const token = localStorage.getItem('token')
      Vue.$http.defaults.headers.common.Authorization = `Bearer ${token}`
    }
  },
  [LOGIN_SUCCESS] (state, data) {
    state.isLoggedIn = true
    localStorage.setItem('token', data.token.result)
    state.username = data.username
    Vue.$http.defaults.headers.common.Authorization = `Bearer ${data.token.result}`
  },
  [LOGOUT] (state) {
    state.isLoggedIn = false
    localStorage.removeItem('token')
    Vue.$http.defaults.headers.common.Authorization = ''
  }
}
