<template>
  <div class="container">
    <form class="login mt-4" @submit.prevent="login">
      <h1>Sign in</h1>
      <input class="form-control mt-4" required v-model="username" 
        type="text" placeholder="Username"/>
      <input class="form-control mt-4" required v-model="password" 
        type="password" placeholder="Password"/>
      <hr/>
      <button type="submit">Login</button>
   </form>
  </div>
</template>

<script>
export default {
  name: 'login',
  data () {
    return {
      username: '',
      password: ''
    }
  },
  methods: {
    async login () {
      this.$store.dispatch('login', {
        username: this.username,
        password: this.password
      }).then(() => {
        console.log('Fez o dispatch com sucesso')
      }).catch((err) => {
        console.log(err)
      })
    }
  }
}
</script>

<style>
</style>