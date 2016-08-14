var gulp = require('gulp');
var concat = require('gulp-concat');
var uglify = require('gulp-uglify');
var minifycss = require('gulp-minify-css');
var Server = require('karma').Server;
var jshint = require('gulp-jshint');
var connect = require('gulp-connect');
var sass = require('gulp-sass');
var merge = require('merge-stream');
var ngAnnotate = require('gulp-ng-annotate');
var sourcemaps = require('gulp-sourcemaps');

// *******************************************

gulp.task('buildApp', function () {
    return gulp.src(['app/src/app.js', 'app/src/**/*.js'])
      .pipe(sourcemaps.init())
      .pipe(ngAnnotate())
      .pipe(concat('app.min.js'))
      //.pipe(uglify())
      .pipe(sourcemaps.write())
      .pipe(gulp.dest('app/dist'));
});

gulp.task('buildVendor', function () {
    return gulp.src([
        'bower_components/jquery/dist/*.min.js',
        'bower_components/moment/min/moment.min.js',
        'bower_components/angular*/*.min.js',
        'bower_components/angular-ui-router/release/*.min.js',
        'bower_components/ng-file-upload/ng-file-upload.min.js'])

      .pipe(concat('vendor.min.js'))
      .pipe(gulp.dest('app/dist'));
});

gulp.task('buildCSS', function () {
    var sassStream,
       cssStream;

    //compile sass
    sassStream = gulp.src('app/src/content/style/app.scss')
        .pipe(sass({
            errLogToConsole: true
        }));

    //select additional css files
    cssStream = gulp.src('bower_components/angular-material/angular-material.css');

    //merge the two streams and concatenate their contents into a single file
    return merge(cssStream, sassStream)
        .pipe(concat('app.min.css'))
        .pipe(minifycss())
        .pipe(gulp.dest('app/dist'));
});

gulp.task('moveHTML', function () {
    return gulp.src(['app/src/**/*.html', 'app/src/**/*.json', 'app/src/**/*.svg'])
      .pipe(gulp.dest('app/dist'));
});

gulp.task('build', ['buildApp', 'buildVendor', 'buildCSS', 'moveHTML']);

gulp.task('jshint', function () {
    return gulp.src(['app/src/**/*.js', 'app/test/unit/**/**.js'])
      .pipe(jshint())
      .pipe(jshint.reporter('jshint-stylish'));
});
// ***************************************

gulp.task('watch', function () {
    gulp.watch('app/src/**/*.js', ['buildApp', 'jshint']);
    gulp.watch('app/test/**/*.js', ['jshint']);
    gulp.watch('app/src/content/style/*.scss', ['buildCSS']);
    gulp.watch('app/src/**/*.html', ['moveHTML']);
});

// *******************************************

gulp.task('default', ['build', 'watch']);