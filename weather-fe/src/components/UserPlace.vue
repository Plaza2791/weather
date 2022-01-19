<template>
  <div class="container">
    <Form v-if="currentUserPlace" :validation-schema="schema" class="mb-3" @submit="updateUserPlace">
      <h3>Redaguoti vietovę</h3>
      <div v-if="this.message !== ''" class="alert alert-danger" role="alert">
        {{ this.message }}
      </div>
      <div class="form-group mb-3">
        <label class="form-label" for="code">Pavadinimas</label>
        <h4>
          {{ currentUserPlace.name }}
        </h4>
      </div>
      <div class="form-group mb-3">
        <label class="form-label" for="description">Aprašymas</label>
        <Field v-model="currentUserPlace.description" as="textarea" class="form-control" name="description"
               rows="3"></Field>
        <p>
          <ErrorMessage class="text-danger" name="description"/>
        </p>
      </div>
      <div class="form-group">
        <button :disabled="loading" class="btn btn-success mr-2">
              <span
                  v-show="loading"
                  class="spinner-border spinner-border-sm"
              ></span>
          Išsaugoti
        </button>

        <button class="btn btn-danger" data-target="#deletePlace" data-toggle="modal" type="button">
          <i class="bi bi-trash-fill"/> Ištrinti
        </button>

        <div id="deletePlace" aria-hidden="true" aria-labelledby="deletePlace" class="modal fade" role="dialog"
             tabindex="-1">
          <div class="modal-dialog" role="document">
            <div class="modal-content">
              <div class="modal-header">
                <h5 id="deletePlaceLabel" class="modal-title">Ištrinti {{ currentUserPlace.name }}</h5>
                <button aria-label="Close" class="close" data-dismiss="modal" type="button">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
              <div class="modal-body">
                Ar tikrai norite ištrinti {{ currentUserPlace.name }}?
              </div>
              <div class="modal-footer">
                <button class="btn btn-secondary" data-dismiss="modal" type="button">Atšaukti</button>
                <button class="btn btn-danger" data-dismiss="modal" @click="removeUserPlace">
                  <i class="bi bi-trash-fill"/> Ištrinti
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </Form>
    <div v-else>
      <p>Pasirinkite vietovę</p>
    </div>
  </div>
</template>

<script>
import UserPlaceDataService from "../services/UserPlaceDataService";
import * as yup from "yup";
import {ErrorMessage, Field, Form} from "vee-validate";

export default {
  components: {
    Form,
    Field,
    ErrorMessage
  },
  data() {
    const schema = yup.object().shape({
      description: yup.string().max(255, "Aprašymas negali būti ilgesnis nei 255 simbolių")
    });

    return {
      loading: false,
      currentUserPlace: null,
      description: '',
      message: '',
      schema
    }
  },
  methods: {
    getUserPlace(code) {
      UserPlaceDataService.get(code)
          .then(response => {
            this.currentUserPlace = response.data;
            console.log(response.data);
          })
          .catch(e => {
            console.log(e);
          });
    },
    updateUserPlace(userPlace) {
      this.message = "";
      this.loading = true;

      console.log(userPlace)

      UserPlaceDataService.update(this.currentUserPlace.code, userPlace)
          .then(() => {
            this.loading = false;
            history.back();
          })
          .catch(error => {
            console.log(error.response.data)
            this.message = error.response.data.error || error.response.data.errors
            this.loading = false;
          });
    },
    removeUserPlace() {
      UserPlaceDataService.delete(this.currentUserPlace.code)
          .then(response => {
            console.log(response.data);
            history.back();
          })
          .catch(e => {
            console.log(e);
          });
    }
  },
  mounted() {
    console.log(this.$route.params)
    this.message = '';
    this.getUserPlace(this.$route.params.code);
  }
}
</script>