pipeline {
  agent any
  stages {
    stage('Build') {
      steps {
        sh 'ls'
      }
    }
    stage('Run') {
      steps {
        echo 'Hello, this is just an message.'
        node(label: 'Allocate node')
        waitUntil() {
          echo 'Signal'
        }
        
      }
    }
  }
}