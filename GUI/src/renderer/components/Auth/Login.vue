<template>
  <div class="container">
  <router-link to='home'>Go Back</router-link>
    <div class="row justify-content-md-center mt-3">
      <h1>Sign in</h1>
    </div>
    <form class="login mt-4 col-md-6 offset-md-3" @submit.prevent="login">
      <input class="form-control mt-4" required v-model="username" 
        type="text" placeholder="Username"/>
      <input class="form-control mt-4" required v-model="password" 
        type="password" placeholder="Password"/>
      <div class="alert alert-danger text-center col-md-12 mt-3 text-sm" role="alert" v-if="showError" v-on:click="toggleError()">
      {{ error }}
      </div>
      <hr/>
      <button class="btn btn-primary" type="submit">Login</button>
   </form>
  </div>
</template>

<script>
import Vue from 'vue'

export default {
  name: 'login',
  data () {
    return {
      username: '',
      password: '',
      showError: false,
      error: 'Username and/or password incorrect!'
    }
  },
  methods: {
    async login () {
      this.$store.dispatch('login', {
        username: this.username,
        password: this.password
      }).then(() => {
        if (this.$store.getters.getRole === 'Solver') {
          Vue.router.push({
            name: 'unassigned_tickets'
          })
        } else {
          this.error = 'You do not have permission to access this department.'
          this.toggleError()
        }
      }).catch((err) => {
        this.error = 'Username and/or password incorrect!'
        this.toggleError()
        console.log('Problem in sign in.')
        console.log(err)
      })
    },
    toggleError () {
      this.showError = !this.showError
    }
  }
}
</script>

<style>
</style>