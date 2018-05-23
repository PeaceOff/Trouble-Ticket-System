export default {
  isLoggedIn: state => {
    return state.isLoggedIn
  },
  getUsername: state => {
    return state.username
  },
  getToken: state => {
    return state.token
  },
  getRole: state => {
    return state.role
  }
}
