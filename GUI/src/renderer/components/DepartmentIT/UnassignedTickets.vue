<template>
  <div class="container">
    <div class="row-md-12">
      <nav class="navbar navbar-expand navbar-light bg-light justify-content-between">
        <div>
          <a class="navbar-brand">Trouble Ticket System</a>
        </div>
        <div>
          <span>Hello {{ username }}!</span>
          <button class="btn btn-secondary btn-sm" @click="logout()">Logout</button>
        </div>
      </nav>
    </div>
    <div class="row justify-content-md-center mt-3">
      <h1>IT Department</h1>
    </div>
    <!-- Menu -->
    <div class="row justify-content-md-center mt-3">
        <nav aria-label="...">
            <ul class="pagination pagination-lg">
                <li class="page-item disabled"><a class="page-link" tabindex="-1">Unassigned Tickets</a></li>
                <li class="page-item"><a class="page-link"><router-link to="my_tickets">My Tickets</router-link></a></li>
                <li class="page-item"><a class="page-link"><router-link to="my_secondary_tickets">My Secondary Tickets</router-link></a></li>
            </ul>
        </nav>
    </div>
    <div class="row justify-content-md-center mt-4">
      <div class="card border-secondary mb-3 col-md-12" v-bind:key="ticket.id" v-for="ticket in tickets">
        <div class="card-body">
          <div class="row">
          <h5 class="card-title col-md-9">{{ ticket.title }}</h5>
          <button class="btn btn-primary col-md-3" type="button" v-on:click="assignTicket(ticket.id)">Assign</button>
          </div>
          <p class="card-text">{{ ticket.description }}</p>
        </div>        
      </div>
    </div>
  </div>
</template>

<script>
import TicketProxy from '@/proxies/TicketProxy'

export default {
  name: 'department_it',
  data () {
    return {
      username: this.$store.getters.getUsername,
      tickets: ''
    }
  },
  async created () {
    try {
      const response = await new TicketProxy().getUnassignedTickets()
      this.tickets = response
    } catch (e) {
      console.log(e)
    }
  },
  methods: {
    logout () {
      this.$store.dispatch('logout')
    },
    async assignTicket (id) {
      try {
        await new TicketProxy().assignTicket(id)
        this.tickets.splice(this.tickets.findIndex(x => x.id === id), 1)
      } catch (e) {
        console.log(e)
      }
    }
  }
}
</script>

<style>
</style>