<template>
  <div class="container col-md-6 col-offset-3">
    <a href="#" v-on:click="goBack()">Go Back</a>
    <div class="row justify-content-md-center mt-3">
      <h1>Create secondary ticket</h1>
    </div>
    <div class="row mt-5">
      <form @submit.prevent="submit" class="col-md-10 offset-md-1">
        <div class="form-group">
          <input type="text" class="form-control" v-model="secondaryTicketTitle" placeholder="Title">
          <small id="emailHelp" class="form-text text-muted">Keep it short and simple.</small>
        </div>
        <div class="form-group">
          <input type="text" class="form-control" v-model="secondaryTicketDescription" placeholder="Description">
        </div>
        <button type="submit" class="btn btn-primary">Submit ticket</button>
      </form>
    </div>
    <div class="alert alert-warning fixed-bottom mx-5 text-center" role="alert" v-if="showAlert" v-on:click="toggleAlert()">
      You must provide an answer before submitting!
    </div>
    <div class="alert alert-danger fixed-bottom mx-5 text-center" role="alert" v-if="showError" v-on:click="toggleError()">
      An error occured with your request. Please try again later!
    </div>
  </div>
</template>

<script>
import SecondaryTicketProxy from '@/proxies/SecondaryTicketProxy'
import Vue from 'vue'

export default {
  name: 'secondaryTicket',
  data () {
    return {
      ticketId: this.$route.params.id,
      secondaryTicketTitle: '',
      secondaryTicketDescription: '',
      showAlert: false,
      showError: false
    }
  },
  methods: {
    async submit () {
      if (this.secondaryTicketTitle && this.secondaryTicketDescription) {
        const data = {
          ticketId: this.ticketId,
          title: this.secondaryTicketTitle,
          description: this.secondaryTicketDescription
        }
        try {
          await new SecondaryTicketProxy().create(data)

          Vue.router.push({
            name: 'my_secondary_tickets'
          })
        } catch (e) {
          this.toggleError()
        }

        // Voltar para o MyTickets.vue || SecondaryTicketList
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
    goBack () {
      Vue.router.go(-1)
    }
  }
}
</script>

<style>
  
</style>