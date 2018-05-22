<template>
  <div class="container">
    <div class="row justify-content-md-center mt-3">
      <h1>Create secondary ticket</h1>
    </div>
    <div class="row">
      <form>
        <div class="form-group">
          <input type="text" class="form-control" v-model="secondaryTicketTitle" placeholder="Title">
          <small id="emailHelp" class="form-text text-muted">Keep it short and simple.</small>
        </div>
        <div class="form-group">
          <input type="text" class="form-control" v-model="secondaryTicketDescription" placeholder="Description">
        </div>
        <button type="submit" class="btn btn-primary" v-on:click="submit()">Submit ticket</button>
      </form>
    </div>
    <div class="alert alert-warning fixed-bottom mx-5 text-center" role="alert" v-if="showAlert">
      You must provide an answer before submitting!
      <button type="button" class="close" data-dismiss="alert" aria-label="Close" v-on:click="toggleAlert()">
          <span aria-hidden="true">&times;</span>
        </button>
    </div>
    <div class="alert alert-danger fixed-bottom mx-5 text-center" role="alert" v-if="showError">
      An error occured with your request. Please try again later!
      <button type="button" class="close" data-dismiss="alert" aria-label="Close" v-on:click="toggleError()">
          <span aria-hidden="true">&times;</span>
        </button>
    </div>
  </div>
</template>

<script>
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
          this.secondaryTicketTitle = this.appendQuestionMark(this.secondaryTicketTitle)
          this.secondaryTicketDescription = this.appendQuestionMark(this.secondaryTicketDescription)
          // TODO: Criar o secondory ticket na API
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
      appendQuestionMark (text) {
        return (text[text.length - 1] !== '?') ? text + '?' : text
      }
    }
  }
</script>

<style>
  
</style>