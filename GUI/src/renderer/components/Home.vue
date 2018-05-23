<template>
  <div class="container justify-content-md-center mt-5">
    <div class="row mt-5">
      <div class="offset-md-2 col-md-4 justify-content-md-center mt-5">
        <router-link to='department'><button type="button" class="btn btn-primary col-md-12">Department</button></router-link>
      </div>     
      <div class="col-md-4 justify-content-md-center mt-5">
        <router-link to='unassigned_tickets'><button type="button" class="btn btn-primary col-md-12">IT Department</button></router-link>
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