<template>
  <div class="container">
    <div class="row justify-content-md-center mt-3">
      <h1>IT Department</h1>
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
    <div class="alert alert-warning fixed-bottom mx-5 text-center" role="alert" v-if="showAlert">
      You must provide an answer before submitting!
      <button type="button" class="close" data-dismiss="alert" aria-label="Close" v-on:click="toggleAlert()">
        <span aria-hidden="true">&times;</span>
      </button>
      </div>
  </div>
</template>

<script>
import Message from '../logic/messageQueue'
import API from '../logic/proxy'

export default {
  name: 'department_it',
  data () {
    return {
      tickets: Message.tickets,
      showAlert: false
    }
  },
  methods: {
    cardClicked: function (id, answer) {
      if (answer) {
        // Submit answer to API
        API.answerSecondaryQuestion(id, answer)
        // Delete ticket has it was already answered
        this.tickets = this.tickets.filter(ticket => ticket.id !== id)
        // Save the tickets to the file
        Message.setTickets(this.tickets)
      } else {
        this.toggleAlert()
      }
    },
    toggleAlert () {
      this.showAlert = !this.showAlert
    }
  }
}
</script>

<style>
</style>
