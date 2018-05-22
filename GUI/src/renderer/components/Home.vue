<template>
  <div class="container">
    <div class="row justify-content-md-center mt-4">
      <div class="col align-self-center">
        <router-link to='department'><button type="button" class="btn btn-secondary">Department</button></router-link>
      </div>
    </div>     
    <div class="row justify-content-md-center mt-4">
      <div class="col align-self-center">
        <router-link to='unassigned_tickets'><button type="button" class="btn btn-secondary">IT Department</button></router-link>
      </div>
    </div> 
  </div>
</template>

<script>
export default {
  name: 'home'
}
</script>

<style>
</style>