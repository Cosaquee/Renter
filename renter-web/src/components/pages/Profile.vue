<template>
    <div class="">

      <div class="box">
        <h1 class="is-size-1">{{ user.userName }}</h1>
        <p>Email: <em>{{ user.email }}</em></p>
      </div>

      <div class="box">
          <h1 class="is-size-1">Roles</h1>
          <p>Roles: <em> {{ user.role.name }}</em></p>
      </div>
    </div>
</template>

<script>
  import { mapGetters } from 'vuex';

  export default {
    computed: mapGetters(['user']),
  };
</script>
