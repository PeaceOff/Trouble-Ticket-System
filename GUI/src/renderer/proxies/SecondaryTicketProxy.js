import Proxy from './Proxy'

class SecondaryTicketProxy extends Proxy {
  /**
   * The constructor for the SecondaryTicket.
   *
   * @param {Object} parameters The query parameters.
   */
  constructor (parameters = {}) {
    super('api/SecondaryTickets', parameters)
  }

  /**
   * Method used to get all secondary tickets created by the solver.
   *
   * @param {String} email The email.
   * @param {String} password The password.
   *
   * @returns {Promise} The result in a promise.
   */
  getSolverSecondaryTickets () {
    return this.submit('get', `${this.endpoint}/SolverSecondaryTickets`)
  }
}

export default SecondaryTicketProxy
