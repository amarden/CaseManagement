// Karma configuration
// Generated on Sat Jan 09 2016 23:10:28 GMT-0500 (Eastern Standard Time)

module.exports = function(config) {
  config.set({

    // base path that will be used to resolve all patterns (eg. files, exclude)
    basePath: '',

    plugins: [
        require('./node_modules/karma-jasmine'),
        require('./node_modules/karma-ng-html2js-preprocessor'),
        require('./node_modules/karma-phantomjs-launcher'),
        require('./node_modules/karma-chrome-launcher'),
        require('./node_modules/karma-firefox-launcher'),
        require('./node_modules/karma-coverage')

    ],

    // frameworks to use
    // available frameworks: https://npmjs.org/browse/keyword/karma-adapter
    frameworks: ['jasmine'],


    // list of files / patterns to load in the browser
    files: [
      'app/dist/vendor.min.js',
      'bower_components/angular-mocks/angular-mocks.js',
      'app/src/**/*.html',
      'app/src/app.js',
      'app/src/**/*.js',
      'app/test/unit/**/*.js',
      //'test/unit/client/ClientCtrlSpec.js',
      //'test/unit/TestHelper.js'
    ],


    // list of files to exclude
    exclude: [
    ],


    // preprocess matching files before serving them to the browser
    // available preprocessors: https://npmjs.org/browse/keyword/karma-preprocessor
    preprocessors: {
        'app/src/**/*.html': ['ng-html2js'],
        'app/src/**/*.js': ['coverage'],

    },

    ngHtml2JsPreprocessor: {
        moduleName: 'templates',
        stripPrefix: 'app/src/'
    },


    // test results reporter to use
    // possible values: 'dots', 'progress'
    // available reporters: https://npmjs.org/browse/keyword/karma-reporter
    reporters: ['progress', 'coverage'],

    coverageReporter: {
        type : 'text'
    },

    // web server port
    port: 9876,


    // enable / disable colors in the output (reporters and logs)
    colors: true,


    // level of logging
    // possible values: config.LOG_DISABLE || config.LOG_ERROR || config.LOG_WARN || config.LOG_INFO || config.LOG_DEBUG
    logLevel: config.LOG_INFO,


    // enable / disable watching file and executing tests whenever any file changes
    autoWatch: true,


    // start these browsers
    // available browser launchers: https://npmjs.org/browse/keyword/karma-launcher
    browsers: [/*'Chrome', 'Firefox',*/ 'PhantomJS'],


    // Continuous Integration mode
    // if true, Karma captures browsers, runs the tests and exits
    singleRun: false
  })
}
