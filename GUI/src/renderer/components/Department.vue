<template>
  <div class="container">
    <router-link to='home'>Go Back</router-link>
    <div class="row justify-content-md-center mt-3">
      <h1>{{ dept.charAt(0).toUpperCase() + dept.slice(1).toLowerCase() }} department</h1>
    </div>
    <div class="row justify-content-md-center mt-4">
      <div class="card border-secondary mb-3 col-md-12" v-bind:key="ticket.id" v-for="ticket in tickets">
        <div class="card-body">
          <h5 class="card-title">{{ ticket.title }}</h5>
          <p class="card-text">{{ ticket.description }}</p>
          <div class="input-group mb-3">
            <input type="text" class="form-control" v-model="ticket.answer" placeholder="Answer" aria-describedby="basic-addon2">
            <div class="input-group-append">
              <button class="btn btn-outline-secondary" type="button" v-on:click="cardClicked(ticket.id,ticket.answer)">Submit</button>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="alert alert-warning fixed-bottom mx-5 text-center" role="alert" v-if="showAlert" v-on:click="toggleAlert()">
      You must provide an answer before submitting!
    </div>
    <div class="alert alert-danger fixed-bottom mx-5 text-center" role="alert" v-if="showError" v-on:click="toggleError()">
      An error occured while submetting the request. Please try again later!
    </div>
    <div class="alert alert-success fixed-bottom mx-5 text-center" role="alert" v-if="showSuccess" v-on:click="toggleSuccess()">
      Answer submitted!
    </div>
  </div>
</template>

<script>
  import Message from '../logic/messageQueue'
  import _Proxy from '../logic/proxy'
  
  export default {
    name: 'department',
    data () {
      return {
        dept: Message.dept,
        tickets: Message.tickets,
        showAlert: false,
        showError: false,
        showSuccess: false
      }
    },
    methods: {
      async cardClicked (id, answer) {
        if (answer) {
          // Submit answer to API
          if (await _Proxy.answerSecondaryQuestion(id, answer)) {
            // Show feedback to the user
            this.toggleSuccess()
            // Delete ticket has it was already answered
            this.tickets = this.tickets.filter(ticket => ticket.id !== id)
            // Save the tickets to the file
            Message.setTickets(this.tickets)
          } else {
            this.toggleError()
          }
        } else {
          this.toggleAlert()
        }
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
  
  .container {
    padding-left: 10%;
    padding-right: 10%;
  }
</style>
