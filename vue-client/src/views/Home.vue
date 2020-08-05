<template>
  <div class="home">
    <h1>Members</h1>

    <table class="table table-striped" v-if="members">
      <thead>
        <tr>
          <th>ID</th>
          <th>Name</th>
          <th>Email</th>
          <th>Actions</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="member in members" :key="member.id">
          <td>{{ member.id }}</td>
          <template v-if="member.id===edit.selected">
            <td>
              <input class="txt-edit" v-model="edit.name">
              <p class="error" v-show="errors.nameRequired">Name is required.</p>
            </td>
            <td>
              <input class="txt-edit" v-model="edit.email">
              <p class="error" v-show="errors.invalidEmail">Email is invalid.</p>
            </td>
            <td class="btn-group">
              <button class="img-btn btn-link" @click="cancelEdit">
                <img src="/cancel.svg" width="12px" height="18px" />
              </button>
              <button class="img-btn btn-link" @click="saveEdit(member)">
                <img src="/save.svg" width="12px" height="12px" />
              </button>
            </td>
          </template>
          <template v-else>
            <td>{{ member.name }}</td>
            <td>{{ member.email }}</td>
            <td class="btn-group">
              <button class="img-btn btn-link" @click="deleteMember(member)">
                <img src="/delete.svg" width="12px" height="12px" />
              </button>
              <button class="img-btn btn-link" @click="editMember(member)">
                <img src="/edit.svg" width="12px" height="12px" />
              </button>
            </td>
          </template>
        </tr>
      </tbody>
    </table>

    <p v-else>
      <em>
        Loading...
      </em>
    </p>
  </div>
</template>

<script>
  export default {
    name: "Home",

    data: function() {
      return {
        members: null,
        edit: {
          name: "",
          email: "",
          selected: null
        }
      };
    },
    computed: {
      errors: function () {
        const errors = {
          nameRequired: false,
          invalidEmail: false
        }

        if(this.edit.name === "")
          errors.nameRequired = true;

        if(this.edit.email !== "" && !this.edit.email.match(/[A-Z0-9+_.-]+@[A-Z0-9.-]+/i))
          errors.invalidEmail = true;

        return errors;
      }
    },
    methods: {
      async deleteMember(member) {
        const response = await this.axios.delete("/members/" + member.id, {method: "DELETE"});
        this.members = this.members.filter(m => m.id !== member.id);
      },

      async editMember(member) {
        this.edit.name = member.name;
        this.edit.email = member.email;
        this.edit.selected = member.id;
      },

      async cancelEdit() {
        this.edit.selected = null;
      },

      async saveEdit(member) {
        if(this.errors.nameRequired || this.errors.invalidEmail)
          return;

        const data = {
          name: this.edit.name,
          email: this.edit.email
        }
        const response = await this.axios.put("/members/" + member.id, data);
        member.name = this.edit.name;
        member.email = this.edit.email;
        this.edit.selected = null;
      }
    },

    async mounted() {
      const response = await this.axios.get("/members");
      this.members = response.data;
    }
  };
</script>

<style scoped>
  p.error {
    color: firebrick;
  }
  .txt-edit {
    width: 100%;
  }
</style>
