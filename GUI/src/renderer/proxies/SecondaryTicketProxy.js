import Proxy from './Proxy'

class SecondaryTicketProxy extends Proxy {
  /**
   * The constructor for the AuthProxy.
   *
   * @param {Object} parameters The query parameters.
   */
  constructor (parameters = {}) {
    super('api/SecondaryTickets', parameters)
  }
}

export default SecondaryTicketProxy
