<template>
  <div id="app">
    <landing-page></landing-page>
  </div>
</template>

<script>
  import LandingPage from '@/components/LandingPage'

  export default {
    name: 'gui',
    components: {
      LandingPage
    }
  }
</script>

<style>
  /* CSS */
  @import url("https://stackpath.bootstrapcdn.com/bootstrap/4.1.1/css/bootstrap.min.css");
</style>
