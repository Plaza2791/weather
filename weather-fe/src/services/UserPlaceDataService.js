import http from "../http-common";

class UserPlaceDataService {
    getAll() {
        return http.get("/userPlaces");
    }

    get(code) {
        return http.get(`/userPlaces/${code}`);
    }

    create(data) {
        return http.post("/userPlaces", data);
    }

    update(code, data) {
        return http.put(`/userPlaces/${code}`, data);
    }

    delete(code) {
        return http.delete(`/userPlaces/${code}`);
    }

    deleteAll() {
        return http.delete(`/userPlaces`);
    }
}

export default new UserPlaceDataService();