import {
  LOGIN,
  LOGIN_SUCCESS,
  LOGOUT
} from './mutations-types'

export default {
  [LOGIN] (state) {
    state.pending = true
  },
  [LOGIN_SUCCESS] (state) {
    state.isLoggedIn = true
    state.pending = false
  },
  [LOGOUT] (state) {
    state.isLoggedIn = false
  }
}
