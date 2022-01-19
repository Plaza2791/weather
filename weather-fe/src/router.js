import {createRouter, createWebHistory} from "vue-router";

const routes = [
    {
        path: "/",
        redirect: "/userPlaces"
    },
    {
        path: "/userPlaces",
        component: () => import("./components/UserPlacesList")
    },
    {
        path: "/userPlaces/:code",
        component: () => import("./components/UserPlace")
    },
    {
        path: "/userPlaces/add",
        component: () => import("./components/AddUserPlace")
    }
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

export default router;