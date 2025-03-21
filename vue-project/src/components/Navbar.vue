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
      <!-- <nav class="bg-plum text-grape">
        WORK IN PROGRESS
        <div class="container mx-auto px-4 md:flex items-center gap-6">
          <div class="flex items-center justify-between md:w-auto w-full">
            *** Logo ***
            <RouterLink class="flex flex-shrink-0 items-center mr-4" to="/">
              <img class="h-10 w-auto" :src="logo" alt="Vue Jobs" />
              <span class="sm:block text-lemon text-md font-bold ml-2"
                >Caitlin Thaeler</span
              >
            </RouterLink>
          </div>
          *** menu button ***
          <div class="sm:hidden flex items-center">
              <button class="menu-button">
                <i class="pi pi-bars text-lemon text-7xl mb-5" style="font-size: 2rem"></i>
              </button>
          </div>
        </div>
      </nav> -->
        <nav class="bg-grape">
      <div class="mx-auto max-w-7xl p-6 sm:p-5">
        <div class="flex flex-row h-auto items-center justify-between">
          <div
            class="flex flex-col md:flex-row w-full items-start md:items-stretch justify-between md:justify-end"
          >
            <!-- Logo -->
            <RouterLink class="flex flex-shrink-0 items-center mr-4" to="/">
              <img class="h-10 w-auto" :src="logo" alt="img" />
              <span class="hidden md:block text-lemon text-md font-bold ml-2"
                >Caitlin Thaeler</span
              >
            </RouterLink>
            <div class="md:ml-auto">
              <div class="flex flex-col md:flex-row gap-4 text-2xl">
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