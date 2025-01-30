<script setup>
import { RouterLink, useRoute } from 'vue-router'
import logo from '@/assets/img/bond-unclicked.png'

const activeTextColor = 'text-lemon-dark'; //on the page
const hoverTextColor = 'hover:text-plum'; //hovering
const inactiveTextColor = 'text-lemon' //normal

const isActiveLink = (routePath) => {
  //gets the current route
  const route = useRoute();
  return route.path === routePath;
}


</script>

<template>
    <div>
        <nav class="bg-grape">
      <div class="mx-auto max-w-7xl px-2 sm:px-6 lg:px-8">
        <div class="flex h-20 items-center justify-between">
          <div
            class="flex flex-1 items-center justify-center md:items-stretch md:justify-start"
          >
            <!-- Logo -->
            <RouterLink class="flex flex-shrink-0 items-center mr-4" to="/">
              <img class="h-10 w-auto" :src="logo" alt="Vue Jobs" />
              <span class="hidden md:block text-lemon text-md font-bold ml-2"
                >Caitlin Thaeler</span
              >
            </RouterLink>
            <div class="md:ml-auto">
              <div class="flex space-x-2 text-2xl">
                <RouterLink
                  to="/"
                  :class="[
                    isActiveLink('/')
                      ? activeTextColor
                      : hoverTextColor, 
                      inactiveTextColor, 
                      'px-3', 
                      'py-2', 
                      'rounded-md'
                    ]"
                  >Home</RouterLink
                >
                <RouterLink
                  to="/projects"
                  :class="[
                    isActiveLink('/projects')
                    ? activeTextColor
                      : hoverTextColor, 
                      inactiveTextColor, 
                      'px-3', 
                      'py-2', 
                      'rounded-md'
                    ]"
                  >Projects</RouterLink
                >
                <!-- <RouterLink
                  to="/jobs"
                  :class="[
                    isActiveLink('/jobs')
                      ? 'bg-green-900 text-lemon-dark'
                      : 'hover:bg-gray-900 hover:text-lemon-light', 
                      'text-lemon', 
                      'px-3', 
                      'py-2', 
                      'rounded-md'
                    ]"
                  >Jobs</RouterLink
                > -->
                <RouterLink
                  to="/experience"
                  :class="[
                    isActiveLink('/experience') ? activeTextColor : inactiveTextColor,
                      hoverTextColor,
                      'px-3', 
                      'py-2', 
                      'rounded-md'
                    ]"
                  >Experience</RouterLink
                >
                <RouterLink
                  to="/about"
                  :class="[
                    isActiveLink('/about') ? activeTextColor : inactiveTextColor,
                      hoverTextColor,
                      'px-3', 
                      'py-2', 
                      'rounded-md'
                    ]"
                  >About</RouterLink
                >
                
              </div>
            </div>
          </div>
        </div>
      </div>
    </nav>
    </div>
</template>