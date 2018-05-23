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
          <li class="page-item"><a class="page-link"><router-link to="unassigned_tickets">Unassigned Tickets</router-link></a></li>
          <li class="page-item"><a class="page-link"><router-link to="my_tickets">My Tickets</router-link></a></li>
          <li class="page-item disabled"><a class="page-link" tabindex="-1">My Secondary Tickets</a></li>
        </ul>
      </nav>
    </div>
    <div class="alert alert-success fixed mx-5 text-center" role="alert" v-if="showSuccess">
      Answer submitted!
      <button type="button" class="close" data-dismiss="alert" aria-label="Close" v-on:click="toggleSuccess()">
          <span aria-hidden="true">&times;</span>
        </button>
    </div>
    <div class="alert alert-warning fixed mx-5 text-center" role="alert" v-if="showAlert">
      You must provide an answer before submitting!
      <button type="button" class="close" data-dismiss="alert" aria-label="Close" v-on:click="toggleAlert()">
          <span aria-hidden="true">&times;</span>
        </button>
    </div>
    <div class="alert alert-danger fixed mx-5 text-center" role="alert" v-if="showError">
      An error occured with your request. Please try again later!
      <button type="button" class="close" data-dismiss="alert" aria-label="Close" v-on:click="toggleError()">
          <span aria-hidden="true">&times;</span>
        </button>
    </div>
    <div class="row justify-content-md-center mt-4">
      <div class="card border-secondary mb-3 col-md-12" v-bind:key="ticket.id" v-for="ticket in tickets">
        <div class="card-body">
          <h5 class="card-title">{{ ticket.title }}</h5>
          <p class="card-text">{{ ticket.description }}</p>
          <br>
          <h6 class="card-text">Department Answer:</h6>
          <p class="card-text">{{ ticket.answer }}</p>
          <p class="card-text" v-if='ticket.answer == null'>No answer yet.</p>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
  import SecondaryTicketProxy from '@/proxies/SecondaryTicketProxy'
  
  export default {
    name: 'department_it',
    data () {
      return {
        username: this.$store.getters.getUsername,
        tickets: '',
        showAlert: false,
        showError: false,
        showSuccess: false
      }
    },
    async created () {
      try {
        const response = await new SecondaryTicketProxy().getSolverSecondaryTickets()
        this.tickets = response
      } catch (e) {
        console.log(e)
      }
    },
    methods: {
      logout () {
        this.$store.dispatch('logout')
      },
      toggleAlert () {
        this.showAlert = !this.showAlert
      },
      toggleError () {
        this.showError = !this.showError
      },
      toggleSuccess () {
        this.showSuccess = !this.showSuccess
      }
    }
  }
</script>

<style>
  
</style>