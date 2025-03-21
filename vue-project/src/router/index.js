import { createRouter, createWebHistory } from 'vue-router';

import DefaultLayout from '@/layouts/DefaultLayout.vue';
import AdminLayout from '@/layouts/AdminLayout.vue';
import MinimalLayout from '@/layouts/MinimalLayout.vue';

import HomeView from '@/views/HomeView.vue'
import NotFoundView from '@/views/NotFoundView.vue'
import ProjectsView from '@/views/ProjectsView.vue'
import ProjectView from '@/views/ProjectView.vue'
import ExperienceView from '@/views/ExperienceView.vue'
import AboutView from '@/views/AboutView.vue'
import ResumeView from '@/views/ResumeView.vue'
import RogueCatView from '@/views/RogueCatView.vue'
import AdminLoginView from '@/views/AdminLoginView.vue';

import AdminDashboard from '@/views/admin/DashboardView.vue'

import { validateToken } from '@/utils/authorization.js';

const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes: [
        { path: '/', name: 'home', component: HomeView, meta: { layout: DefaultLayout } },
        { path: '/projects', name: 'projects', component: ProjectsView, meta: { layout: DefaultLayout } },
        { path: '/projects/67936e6cfd00290dbff739d8', name: 'rogue-cat', component: RogueCatView, meta: { layout: DefaultLayout } },
        { path: '/projects/:id', name: 'project', component: ProjectView, meta: { layout: DefaultLayout } },
        { path: '/experience', name: 'experience', component: ExperienceView, meta: { layout: DefaultLayout } },
        { path: '/about', name: 'about', component: AboutView, meta: { layout: DefaultLayout } },
        { path: '/resume', name: 'resume', component: ResumeView, meta: { layout: DefaultLayout } },
        { path: '/admin/login', name: 'adminLogin', component: AdminLoginView, meta: { layout: MinimalLayout } },
        { path: '/admin/dashboard', name: 'adminDashboard', component: AdminDashboard, meta: { layout: AdminLayout, requiresAuth: true } },
        { path: '/:catchAll(.*)', name: 'not-found', component: NotFoundView, meta: { layout: DefaultLayout } },
    ],
});

router.beforeEach(async (to, from, next) => {
    if (to.meta.requiresAuth) {
        try {
            const isValid = await validateToken();
            if (isValid) {
                return next();
            }
            console.log('invalid token');
            return next({ name: 'adminLogin'});
        }
        catch (error) {
            console.log('error trying to validate token', error.message);
            return next({ name: 'adminLogin'});
        }
    }
    next();
});

export default router;