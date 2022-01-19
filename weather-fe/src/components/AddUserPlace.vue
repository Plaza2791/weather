<template>
  <div class="container">
    <Form :validation-schema="schema" class="mb-3" @submit="newUserPlace">
      <h3>Pridėti vietovę</h3>
      <div v-if="this.message !== ''" class="alert alert-danger" role="alert">
        {{ this.message }}
      </div>
      <div class="form-group mb-3">
        <label class="form-label" for="code">Pavadinimas</label>
        <Field as="select" class="custom-select custom-select-lg mb-3" name="code"
               @change="this.code = $event.target.value; this.name = places.find(p => p.code == this.code).name">
          <option hidden selected></option>
          <option v-for="(place, index) in places" :key="index" :value="place.code">{{ place.name }}</option>
        </Field>
        <p hidden>
          <Field v-model="name" name="name" type="text">{{ this.name }}</Field>
        </p>
      </div>
      <div class="form-group mb-3">
        <label class="form-label" for="description">Aprašymas</label>
        <Field as="textarea" class="form-control" name="description" rows="3"></Field>
        <p>
          <ErrorMessage class="text-danger" name="description"/>
        </p>
      </div>
      <div class="form-group">
        <button :disabled="loading" class="btn btn-success btn-block">
              <span
                  v-show="loading"
                  class="spinner-border spinner-border-sm"
              ></span>
          Išsaugoti
        </button>
      </div>
    </Form>
  </div>
</template>

<script>
import UserPlaceDataService from "../services/UserPlaceDataService";
import PlaceDataService from "../services/PlaceDataService";
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
      code: yup.string(),
      name: yup.string(),
      description: yup.string().max(255, "Aprašymas negali būti ilgesnis nei 255 simbolių")
    });

    return {
      loading: false,
      code: '',
      name: '',
      description: '',
      message: '',
      schema,
      places: []
    }
  },
  methods: {
    newUserPlace(userPlace) {
      this.message = "";
      this.loading = true;

      UserPlaceDataService.create(userPlace)
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
    getPlaces() {
      PlaceDataService.getAll()
          .then(response => {
            this.places = response.data;
            console.log(response.data);
          });
    }
  },
  mounted() {
    this.message = '';
    this.getPlaces();
  }
}
</script>