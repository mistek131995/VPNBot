pipeline {
    agent any

    stages {
        stage('Download source') {
            steps {
                sh 'mkdir /home/source'
				sh 'git clone https://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@github.com/mistek131995/VPNBot.git /home/source'
            }
        }
        stage('Create image') {
            steps {
                sh 'docker build -f /home/source'
            }
        }
		stage('Stop container') {
            steps {
                echo 'Testing..'
            }
        }
		stage('Start container') {
            steps {
                echo 'Testing..'
            }
        }
        stage('Delete source') {
            steps {
                sh 'rm -R /home/source'
            }
        }
    }
}