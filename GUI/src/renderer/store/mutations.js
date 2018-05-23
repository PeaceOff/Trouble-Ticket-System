import Vue from 'vue'
import {
  LOGIN_SUCCESS,
  LOGOUT
} from './mutations-types'

export default {
  [LOGIN_SUCCESS] (state, data) {
    state.isLoggedIn = true
    state.token = data.token.result
    state.username = data.username
    Vue.$http.defaults.headers.common.Authorization = `Bearer ${data.token.result}`
  },
  [LOGOUT] (state) {
    state.isLoggedIn = false
    state.token = null
    state.username = null
    Vue.$http.defaults.headers.common.Authorization = ''
  }
}
