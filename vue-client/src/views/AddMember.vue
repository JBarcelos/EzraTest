<template>
  <div>
    <h1>Add Member</h1>

    <div class="div-input">
      <label for="name">Name:</label><input v-model="name" name="name" />
      <p class="error" v-show="errors.nameRequired">Name is required.</p>
    </div>
    <div class="div-input">
      <label for="email">Email:</label><input v-model="email" name="email" />
      <p class="error" v-show="errors.invalidEmail">Email is invalid.</p>
    </div>
    <button class="btn btn-primary" @click="addMember">
      Add
    </button>
  </div>
</template>

<script>
  export default {
    name: "AddMember",
    data: function() {
      return {
        name: "",
        email: ""
      }
    },
    computed: {
      errors: function () {
        const errors = {
          nameRequired: false,
          invalidEmail: false
        }

        if(this.name === "")
          errors.nameRequired = true;

        if(this.email !== "" && !this.email.match(/[A-Z0-9+_.-]+@[A-Z0-9.-]+/i))
          errors.invalidEmail = true;

        return errors;
      }
    },
    methods: {
      async addMember() {
        if(this.errors.nameRequired || this.errors.invalidEmail)
          return;

        const member = {
          name: this.name,
          email: this.email
        }
        const response = await this.axios.post("/members/test", member);
        this.$router.push('/')
      }
    }
  };
</script>

<style scoped>
  p.error {
    display: inline;
    color: firebrick;
    margin-left: 1em;
  }
  div.div-input > label {
    width: 3em;
    text-align: right;
    padding-right: 0.5em;
    margin: 1em 0;
  }
</style>
