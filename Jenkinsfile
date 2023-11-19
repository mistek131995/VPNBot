pipeline {
    agent any

    stages {
        stage('Download source') {
            steps {
				script {
					res = sh(script: "test -d /home/source && echo '1' || echo '0' ", returnStdout: true).trim()
					if(res=='1'){
						sh 'rm -R /home/source'
					}				
				}
			
                sh 'mkdir /home/source'
				sh 'git clone https://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@github.com/mistek131995/VPNBot.git /home/source'
            }
        }
        stage('Create image') {
            steps {
                sh 'docker build -t vpn-api-${BUILD_NUMBER} /home/source'
            }
        }
		stage('Stop container') {
            steps {
                script{
                    def containerId = sh (script: 'docker ps -q -f name=vpn-api', returnStdout: true)

                    if(containerId != ''){
                        sh "docker stop ${containerId}"           
                    }
                }
            }
        }
		stage('Start container') {
            steps {
                sh "docker run -d --name=vpn-api-${BUILD_NUMBER} --restart=always vpn-api-${BUILD_NUMBER}"
            }
        }
        stage('Delete source') {
            steps {
                sh 'rm -R /home/source'
            }
        }
    }
}