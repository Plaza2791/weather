<template>
  <nav class="navbar navbar-expand-lg navbar-dark bg-dark">
    <router-link class="navbar-brand" to="/">Orų prognozės</router-link>
    <button aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation" class="navbar-toggler"
            data-target="#navbarSupportedContent" data-toggle="collapse" type="button">
      <span class="navbar-toggler-icon"></span>
    </button>

    <div id="navbarSupportedContent" class="collapse navbar-collapse">
      <ul class="navbar-nav mr-auto">
        <li class="nav-item active">
          <router-link class="nav-link" to="/userPlaces">Vietovės</router-link>
        </li>
        <li class="nav-item">
          <router-link class="nav-link" to="/userPlaces/add">Pridėti vietovę</router-link>
        </li>
      </ul>
    </div>
  </nav>

  <div class="container mt-3">
    <router-view/>
  </div>
</template>

<script>
export default {
  name: 'App'
}
</script>
