<template>
  <div id="app">
    <department></department>
  </div>
</template>

<script>
  import Department from '@/components/Department'

  export default {
    name: 'gui',
    components: {
      Department
    }
  }
</script>

<style>
  /* CSS */
  @import url("https://stackpath.bootstrapcdn.com/bootstrap/4.1.1/css/bootstrap.min.css");
</style>
