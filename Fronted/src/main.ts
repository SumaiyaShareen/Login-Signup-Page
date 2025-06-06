// src/polyfills.ts (or src/main.ts if polyfills.ts doesn't exi// Should contain this import:
import 'zone.js'; 
import { platformBrowser } from '@angular/platform-browser';
import { AppModule } from './app/app-module';



platformBrowser().bootstrapModule(AppModule, {
  
})
  .catch(err => console.error(err));
