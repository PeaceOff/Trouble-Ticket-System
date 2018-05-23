import Proxy from './Proxy'

class AuthProxy extends Proxy {
  /**
   * The constructor for the AuthProxy.
   *
   * @param {Object} parameters The query parameters.
   */
  constructor (parameters = {}) {
    super('api/Account', parameters)
  }

  /**
   * Method used to login.
   *
   * @param {String} email The email.
   * @param {String} password The password.
   *
   * @returns {Promise} The result in a promise.
   */
  login (data) {
    return this.submit('post', `${this.endpoint}/login`, data)
  }

  /**
   * Method used to logout.
   *
   * @returns {Promise} The result in a promise.
   */
  logout () {
    return this.submit('post', `${this.endpoint}/logout`)
  }
}

export default AuthProxy
