import Proxy from './Proxy'

class TicketProxy extends Proxy {
  /**
   * The constructor for the AuthProxy.
   *
   * @param {Object} parameters The query parameters.
   */
  constructor (parameters = {}) {
    super('api/Tickets', parameters)
  }

  /**
   * Method used to get all unassigned tickets.
   *
   * @param {String} email The email.
   * @param {String} password The password.
   *
   * @returns {Promise} The result in a promise.
   */
  getUnassignedTickets () {
    return this.submit('get', `${this.endpoint}/UnassignedTickets`)
  }
}

export default TicketProxy
