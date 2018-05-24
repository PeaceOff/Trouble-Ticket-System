import Proxy from './Proxy'

class TicketProxy extends Proxy {
  /**
   * The constructor for the TicketProxy.
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

  /**
   * Method used to get all tickets assigned to the solver.
   *
   * @param {String} email The email.
   * @param {String} password The password.
   *
   * @returns {Promise} The result in a promise.
   */
  getSolverTickets () {
    return this.submit('get', `${this.endpoint}/AssignedUnsolved`)
  }

  /**
   * Method used to assign a ticket to the solver
   *
   * @returns {Promise} The result in a promise.
   */
  assignTicket (id) {
    return this.submit('put', `${this.endpoint}/AssignTicket/${id}`)
  }
}

export default TicketProxy
