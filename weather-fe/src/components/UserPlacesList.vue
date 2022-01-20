<template>
  <div class="container">
    <div class="row">
      <div class="col-md-12 d-flex pb-4">
        <h1 class="me-2">Vietovių sąrašas</h1>
      </div>
    </div>

    <div class="table-responsive">
      <table class="table table-hover">
        <thead>
        <tr>
          <th scope="col">Pavadinimas</th>
          <th scope="col">Aprašymas</th>
          <th scope="col">Aukščiausia temperatūra</th>
          <th scope="col">Žemiausia temperatūra</th>
          <th scope="col"></th>
          <th scope="col"></th>
        </tr>
        </thead>
        <tbody>
        <tr v-for="(userPlace, index) in userPlaces" :key="index">
          <td style="word-break:break-all;">{{ userPlace.name }}</td>
          <td style="word-break:break-all;">
            <p style="
              word-wrap: break-word;">
              {{ userPlace.description }}
            </p>
          </td>
          <td style="word-break:break-all;">{{ userPlace.maxTemp }}</td>
          <td style="word-break:break-all;">{{ userPlace.minTemp }}</td>
          <td style="word-break:break-all;">
            <router-link
                :to="`/userPlaces/${userPlace.code}`" class="btn btn-warning" type="button">
              <i class="bi bi-pencil-square"/>
            </router-link>
          </td>
          <td style="word-break:break-all;">
            <button :data-target="`#delete${userPlace.code}`" class="btn btn-danger" data-toggle="modal" type="button">
              <i class="bi bi-trash-fill"/>
            </button>

            <div :id="`delete${userPlace.code}`" :aria-labelledby="`delete${userPlace.code}`" aria-hidden="true"
                 class="modal fade"
                 role="dialog" tabindex="-1">
              <div class="modal-dialog" role="document">
                <div class="modal-content">
                  <div class="modal-header">
                    <h5 :id="`delete${userPlace.code}Label`" class="modal-title">Ištrinti {{ userPlace.name }}</h5>
                    <button aria-label="Close" class="close" data-dismiss="modal" type="button">
                      <span aria-hidden="true">&times;</span>
                    </button>
                  </div>
                  <div class="modal-body">
                    Ar tikrai norite ištrinti {{ userPlace.name }}?
                  </div>
                  <div class="modal-footer">
                    <button class="btn btn-secondary" data-dismiss="modal" type="button">Atšaukti</button>
                    <button class="btn btn-danger" data-dismiss="modal" @click="removeUserPlace(userPlace.code)">
                      <i class="bi bi-trash-fill"/> Ištrinti
                    </button>
                  </div>
                </div>
              </div>
            </div>
          </td>
        </tr>
        </tbody>
      </table>
    </div>

    <button class="mt-4 btn btn-danger" data-target="#deleteAll" data-toggle="modal" type="button">
      <i class="bi bi-trash-fill"/> Ištrinti visus miestus
    </button>

    <div id="deleteAll" aria-hidden="true" aria-labelledby="deleteAll" class="modal fade" role="dialog" tabindex="-1">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 id="deleteAllLabel" class="modal-title">Ištrinti visus miestus</h5>
            <button aria-label="Close" class="close" data-dismiss="modal" type="button">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            Ar tikrai norite ištrinti visus miestus?
          </div>
          <div class="modal-footer">
            <button class="btn btn-secondary" data-dismiss="modal" type="button">Atšaukti</button>
            <button class="btn btn-danger" data-dismiss="modal" @click="removeAllUserPlaces">
              <i class="bi bi-trash-fill"/> Ištrinti visus miestus
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import UserPlaceDataService from "../services/UserPlaceDataService";

export default {
  name: "UserPlacesList",
  data() {
    return {
      userPlaces: []
    };
  },
  methods: {
    retrieveUserPlaces() {
      UserPlaceDataService.getAll()
          .then(response => {
            this.userPlaces = response.data;
            console.log(response.data);
          })
          .catch(e => {
            console.log(e);
          });
    },
    refreshList() {
      this.retrieveUserPlaces();
    },
    removeAllUserPlaces() {
      UserPlaceDataService.deleteAll()
          .then(response => {
            console.log(response.data);
            this.refreshList();
          })
          .catch(e => {
            console.log(e);
          });
    },
    removeUserPlace(code) {
      UserPlaceDataService.delete(code)
          .then(response => {
            console.log(response.data);
            this.refreshList();
          })
          .catch(e => {
            console.log(e);
          });
    }
  },
  mounted() {
    this.retrieveUserPlaces();
  }
};
</script>