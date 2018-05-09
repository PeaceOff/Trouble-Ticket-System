<template>
  <div class="container">
    <div class="row justify-content-md-center mt-3">
      <h1>{{ dept.toUpperCase() }} department</h1>
    </div>
    <div class="row justify-content-md-center mt-4">
      <div class="card border-secondary mb-3 col-md-12" v-bind:key="ticket.id" v-for="ticket in tickets" v-on:click="cardClicked(ticket.id,ticket.title,ticket.description)">
        <!-- <div class="card-header">{{ ticket.id}}</div> -->
        <div class="card-body">
          <h5 class="card-title">{{ ticket.title }}</h5>
          <p class="card-text">{{ ticket.description }}</p>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import Message from '../../messageQueue'

export default {
  name: 'landing-page',
  data () {
    return {
      dept: Message.dept,
      tickets: [
        {
          id: 0,
          title: 'bom dia',
          description: 'muito bonito'
        },
        {
          id: 1,
          title: 'bom dia',
          description: 'muito bonito'
        },
        {
          id: 2,
          title: 'bom dia',
          description: 'muito bonito'
        },
        {
          id: 3,
          title: 'bom dia',
          description: 'muito bonito'
        }
      ]
    }
  },
  methods: {
    cardClicked: function (id, title, description) {
      console.log(id)
      console.log(title)
      console.log(description)
    }
  }
}
</script>

<style>
</style>
