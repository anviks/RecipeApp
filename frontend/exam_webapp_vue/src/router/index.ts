import { createRouter, createWebHistory } from 'vue-router';
import Home from '@/views/Home.vue';
import Login from '@/views/account/Login.vue';
import Register from '@/views/account/Register.vue';
import Samples from '@/views/samples/Samples.vue';
import SampleDetails from '@/views/samples/SampleDetails.vue';
import SampleEdit from '@/views/samples/SampleEdit.vue';
import SampleDelete from '@/views/samples/SampleDelete.vue';
import SampleCreate from '@/views/samples/SampleCreate.vue';


const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes: [
        { path: '/', name: 'Home', component: Home },
        { path: '/account/login', name: 'Login', component: Login },
        { path: '/account/register', name: 'Register', component: Register },

        { path: '/samples', name: 'Samples', component: Samples },
        { path: '/samples/:id', name: 'SampleDetails', component: SampleDetails },
        { path: '/samples/:id/edit', name: 'SampleEdit', component: SampleEdit },
        { path: '/samples/:id/delete', name: 'SampleDelete', component: SampleDelete },
        { path: '/samples/create', name: 'SampleCreate', component: SampleCreate },
    ]
});

export default router;
